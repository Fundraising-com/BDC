using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PAToOrderMappingData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            OrderExpressDataContext db = new OrderExpressDataContext();

            #region Delete existing records

            List<program_agreement_order> paoList = 
                (from pao in db.program_agreement_orders
                 select pao).ToList();

            db.program_agreement_orders.DeleteAllOnSubmit(paoList);
            db.SubmitChanges();

            #endregion

            #region Generate new records

            var frfList = from frf in db.form_requires_forms
                          select frf;

            foreach (form_requires_form frf in frfList)
            {
                if (frf.Form.entity_type_id == 4 && frf.RequiredForm.entity_type_id == 12)
                {
                    #region get pa from required form

                    var paList = from pa in db.program_agreements
                                 where pa.deleted == false
                                    && pa.form_id == frf.required_form_id
                                 select pa;

                    foreach (program_agreement pa in paList)
                    {
                        #region get campaign

                        var campaign = (from pac in pa.program_agreement_campaigns
                                        where pac.program_agreement_id == pa.program_agreement_id
                                        select pac).FirstOrDefault();

                        int campaignId = campaign.campaign_id;

                        #endregion

                        #region Get orders from campaign and form

                        var orderList = from o in db.orders
                                        where o.deleted == false
                                            && o.campaign_id == campaignId
                                            && o.form_id == frf.form_id
                                        select o;

                        foreach (order order in orderList)
                        {
                            #region save record

                            program_agreement_order pao = new program_agreement_order();

                            pao.order_id = order.order_id;
                            pao.program_agreement_id = pa.program_agreement_id;
                            pao.deleted = false;
                            pao.create_date = DateTime.Now;
                            pao.create_user_id = 101689;
                            pao.update_date = DateTime.Now;
                            pao.update_user_id = 101689;

                            db.program_agreement_orders.InsertOnSubmit(pao);
                            db.SubmitChanges();

                            #endregion

                            string text = string.Format("order form = {0}, pa form = {1} - order id = {2}, pa id = {3}", order.form_id, pa.form_id, order.order_id, pa.program_agreement_id);
                            sb.AppendLine(text);
                            System.Diagnostics.Debug.WriteLine(text);
                        }

                        #endregion
                    }

                    #endregion
                }
            }

            #endregion

            this.textBox1.Text = sb.ToString();

        }
    }
}

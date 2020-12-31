/*
 * FileName:  QDAServicedComponent
 * 
 *
 */


using System;

using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Reflection;
using QCommon;
using Debug = System.Diagnostics.Debug;
using StackTrace = System.Diagnostics.StackTrace;

[assembly: ApplicationName("QSPFulfillmentDAL")]

namespace DAL
{
    /// <summary>
    /// QDA.
    /// caches the database connection
    /// </summary>
    [JustInTimeActivation]
    //    [ObjectPooling(Enabled=true)]
    [ConstructionEnabled(Default= "Data Source=161.230.158.127;User Id=;Password='';Initial Catalog=QSPCanadaProduct;")]
    abstract public class QDAServicedComponent : ServicedComponent, IQError
    {
        protected string zConstructStringM;
        public string ConstructString
        {
            get{ return this.zConstructStringM; }
            set{ this.zConstructStringM = value; }
        }

        protected SqlConnection connection = null;

        protected int nErrorCode;
        protected string zErrorCode;

        public int GetCode( ){return nErrorCode;}
        public void SetCode(int nCodeP) { nErrorCode = nCodeP;}

        public QDAServicedComponent()
        {
            connection = null;
            zErrorCode="";
            this.ConstructString = "";
        }
         protected override void Activate()
        {
             try
             {
                 if(connection == null)
                 {
                     //zConstructStringM = "Data Source=10.100.106.77;User Id=sa;Password='oneteam';Initial Catalog=QCore;" ;
                     connection = new SqlConnection( zConstructStringM );
                     //"Data Source=KARENTNT;User Id=sa;Password='read0618';Initial Catalog=QCore;" 
                     connection.Open();
                 }
             }
            catch(InvalidOperationException)
            {              			
                SetCode(1);
            }
            catch(SqlException)
            {
                SetCode(1);
            }
            catch(Exception)
            {
                SetCode(1);
            }
        }
       

         protected override void Deactivate()
        {
             try
             {
                 if(connection != null)
                 {
                     connection.Close();
                     connection.Dispose();
                     connection = null;
                 }
             }
             catch(InvalidOperationException)
             {              			
                 SetCode(1);
             }
             catch(SqlException)
             {
                 SetCode(1);
             }
             catch(Exception)
             {
                 SetCode(1);
             }

        }
         protected override  void Construct(string s)
        {
             this.ConstructString=s;
            
        }

		protected SqlParameter[] CreateSqlParameters(MethodInfo method, object[] values)
		{
			if (method == null)
				method = (MethodInfo) (new StackTrace().GetFrame(1).GetMethod());
			ParameterInfo[] methodParameters = method.GetParameters();
            
			int paramIndex = 0;
			foreach (ParameterInfo paramInfo in methodParameters)
			{
				if (Attribute.IsDefined(paramInfo, typeof(NonCommandParameterAttribute)))
					continue;

				//
				// Get the SqlParameter attribute on the parameter.
				// If it is absent, then create one here and use its default settings.
				//

				SqlParameterAttribute paramAttribute = (SqlParameterAttribute) Attribute.GetCustomAttribute(
					paramInfo, typeof(SqlParameterAttribute));
				if (paramAttribute != null)
					paramIndex++;
			}
			Debug.Assert(paramIndex <= values.Length);
			SqlParameter[] aParams = new SqlParameter[paramIndex];
			paramIndex = 0;
			foreach (ParameterInfo paramInfo in methodParameters)
			{
				//
				// If the parameter is tagged with the [NonCommandParameter ]
				// attribute then skip it completely.
				//

				if (Attribute.IsDefined(paramInfo, typeof(NonCommandParameterAttribute)))
					continue;

				//
				// Get the SqlParameter attribute on the parameter.
				// If it is absent, then create one here and use its default settings.
				//

				SqlParameterAttribute paramAttribute = (SqlParameterAttribute) Attribute.GetCustomAttribute(
					paramInfo, typeof(SqlParameterAttribute));

				if (paramAttribute == null)
					paramAttribute = new SqlParameterAttribute();

				//
				// Setup the stored procedure parameter object using 
				// settings from the attribute. Use those values that 
				// were defined otherwise infer them from the method's
				// parameter.
				//

				SqlParameter sqlParameter = new SqlParameter();

				if (paramAttribute.IsNameDefined)
					sqlParameter.ParameterName = paramAttribute.Name;
				else
					sqlParameter.ParameterName = paramInfo.Name;

				if (!sqlParameter.ParameterName.StartsWith("@"))
					sqlParameter.ParameterName = "@" + sqlParameter.ParameterName;

				if (paramAttribute.IsTypeDefined)
					sqlParameter.SqlDbType = paramAttribute.SqlDbType;

				if (paramAttribute.IsSizeDefined)
					sqlParameter.Size = paramAttribute.Size;

				if (paramAttribute.IsScaleDefined)
					sqlParameter.Scale = paramAttribute.Scale;

				if (paramAttribute.IsPrecisionDefined)
					sqlParameter.Precision = paramAttribute.Precision;

				if (paramAttribute.IsDirectionDefined)
				{
					sqlParameter.Direction = paramAttribute.Direction;
				}
				else
				{
					if (paramInfo.ParameterType.IsByRef)
					{
						sqlParameter.Direction = paramInfo.IsOut ? 
							ParameterDirection.Output : 
							ParameterDirection.InputOutput;
					}
					else
					{
						sqlParameter.Direction = ParameterDirection.Input;
					}
				}

				//
				// Sanity check: Too few values supplied?
				//
/*
				Debug.Assert(paramIndex < values.Length);

				sqlParameter.Value = values[paramIndex];
				command.Parameters.Add(sqlParameter);
*/
				sqlParameter.Value = values[paramIndex];
				aParams[paramIndex] = sqlParameter;
				paramIndex++;
			}

			//
			// Sanity check: Too many values supplied?
			//

//			Debug.Assert(paramIndex == values.Length);
			return aParams;

		}

    }
}

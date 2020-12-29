using System.Collections.Generic;
using GA.BDC.Data.Fundraising.FastFundraising.Tables;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Data.Fundraising.FastFundraising.Mappers
{
   public static class RepresentativeMapper
    {
        public static Representative Hydrate(FC fc, IEnumerable<fc_testimonial> testimonials)
        {
            var result = new Representative
            {
                Id = fc.id,
                Name = fc.name,
                AmountRaised = fc.profit_raised != null ? (double)fc.profit_raised : 0.0,
                City = fc.city,
                Email = fc.email_address,
                IsActive = fc.active == 1,
                Phone = fc.phone,
                SAPAccount = fc.SAPAccountNo ?? 0,
                State = fc.state,
                Redirect = fc.login,
                Image = fc.image_url,
                ExternalId = fc.ext_id,
                PartnerId = fc.esubs_parnter_id ?? 857
            };
            foreach (var testimonial in testimonials)
            {
                var t = HydrateTestimonial(testimonial);
                result.Testimonials.Add(t);
            }
            return result;
        }

        private static Testimonial HydrateTestimonial(fc_testimonial testimonial)
        {
            return new Testimonial
            {
                Id = testimonial.id,
                RepresentativeId = (int) testimonial.fc_id,
                Created = testimonial.created_date,
                Account = testimonial.account,
                Author = testimonial.commentor,
                Message = testimonial.comments
            };
        }
    }
}

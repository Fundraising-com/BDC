select pd.Remit_Rate, pd.BasePriceSansPostage, pd.BaseRemitRate, pd.PostageAmount, pd.PostageRemitRate, pd.ConversionRate, ((ISNULL(BasePriceSansPostage, 0.00) * ISNULL(BaseRemitRate, 0.00)) + (ISNULL(PostageAmount, 0.00) * ISNULL(PostageRemitRate, 1.00))) / (ISNULL(BasePriceSansPostage, 0.00) + ISNULL(PostageAmount, 0.00)) aaa, *
from PRICING_DETAILS pd
join Product p on p.Product_Instance = pd.Product_Instance
where p.Type = 46001
and p.Product_Year = 2015
and BasePriceSansPostage > 0.00
and (((ISNULL(BasePriceSansPostage, 0.00) * ISNULL(BaseRemitRate, 0.00)) + (ISNULL(PostageAmount, 0.00) * ISNULL(PostageRemitRate, 1.00))) / (ISNULL(BasePriceSansPostage, 0.00) + ISNULL(PostageAmount, 0.00))) not between ISNULL(Remit_Rate, 0.00) - 0.01 and ISNULL(Remit_Rate, 0.00) + 0.01
--and p.Product_Code = '0338'

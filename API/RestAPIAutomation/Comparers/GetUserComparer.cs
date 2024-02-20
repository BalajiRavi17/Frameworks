namespace APIAutomation.RestAPIAutomation.Comparers
{
    public class GetUserComparer
    {  
        public static bool compareResults(Root x, Root y) 
        {
            if (x == null || y == null) return false;
            if (x.per_page!=y.per_page)  return false; 
            if(x.total_pages!=y.total_pages) return false;
            if(x.page!=y.page) return false;
            if (SupportValidation.UserSupportValidation(x.support, y.support) != true) return false;
            if (DatumValidation.datumValidation(x.data,y.data)!=true) return false;
            return true;
        }
    }
    public class SupportValidation
    {
        public static bool UserSupportValidation(Support x, Support y)
        {
            if (x == null || y == null) { return false; }
            if (x.text != y.text) { return false; }
            if (x.url != y.url) { return false; }
            return true;
        }
    }
    public class DatumValidation
    {
        public static bool datumValidation(List<Datum> x, List<Datum> y)
        {
            if (x.Count() != y.Count()) return false;
            for (int i = 0; i < x.Count(); i++)
            {
                if (x[i].avatar != y[i].avatar) return false;
                if (x[i].id != y[i].id) return false;
                if (x[i].last_name != y[i].last_name) return false;
                if (x[i].first_name != y[i].first_name) return false;
                if (x[i].email != y[i].email) return false;
            }
            return true;
        }
    }
}

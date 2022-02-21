using DAL.Model;
using Entities;

namespace E_HousingAutomation.Controllers.DBControllers
{
    public class AuthDBController
    {
        private DBContext _landlordContext = new DBContext();

        Logger _logger = new Logger();

        public void CreateLogin(APIAuthority loginAdmin)
        {
            _landlordContext.APIAuthority.Add(loginAdmin);
            _landlordContext.SaveChanges();
        }

        public APIAuthority GetLogin(APIAuthority loginAdmin)
        {
            APIAuthority? authority = new APIAuthority();
            if (!string.IsNullOrEmpty(loginAdmin.Email) && !string.IsNullOrEmpty(loginAdmin.Password))
            {
                authority = _landlordContext.APIAuthority.FirstOrDefault(m => m.Email == loginAdmin.Email && m.Password == loginAdmin.Password);
            }

            return authority;
        }

        #region LandlordFunc...

        //find landlord
        public Landlord? FindLandlord(int _id = 0, string _email = "")
        {
            Landlord? landlord = new Landlord();
            if (!string.IsNullOrEmpty(_email))
            {
                landlord = _landlordContext.Landlord.FirstOrDefault(l => l.email == _email);
            }
            else if (_id > 0)
            {
                landlord = _landlordContext.Landlord.FirstOrDefault(l => l.id == _id);
            }
            return landlord;
        }
        #endregion
    }
}

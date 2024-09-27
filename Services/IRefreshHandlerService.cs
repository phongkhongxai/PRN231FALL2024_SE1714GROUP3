using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRefreshHandlerService
    {
        public void GenerateRefreshToken(RefreshToken refreshToken);
        public void ResetRefreshToken();
        public RefreshToken GetRefreshToken(string refreshToken);
        public RefreshToken GetRefreshTokenByEmployeeId(string employeeId);
        public void UpdateRefreshToken(RefreshToken refreshToken);

    }
}

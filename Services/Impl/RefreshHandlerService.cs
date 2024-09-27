using BusinessObjects.Entity;
using DAL.Repositories;
using DAL.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class RefreshHandlerService : IRefreshHandlerService
    {
        private readonly IRefreshHandlerRepository _refreshTokenRepository;
        public RefreshHandlerService(IRefreshHandlerRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }
        public void GenerateRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public RefreshToken GetRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public RefreshToken GetRefreshTokenByEmployeeId(string employeeId)
        {
            throw new NotImplementedException();
        }

        public void ResetRefreshToken()
        {
            throw new NotImplementedException();
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}

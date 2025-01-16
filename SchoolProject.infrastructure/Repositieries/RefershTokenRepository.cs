using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.infrastructure.Abstract;
using SchoolProject.infrastructure.Data;
using SchoolProject.infrastructure.InfastructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.infrastructure.Repositieries
{
    public class RefershTokenRepository:GenericRepositoryAsync<UserRefreshToken>,IRefershTokenRepository
    {
        #region fields
        private DbSet<UserRefreshToken> userRefreshTokens;
        #endregion

        #region Ctor

        public RefershTokenRepository(APPDBContext dbContext) : base(dbContext)
        {
            userRefreshTokens = dbContext.Set<UserRefreshToken>();
        }
        #endregion
        #region handelFunction

        #endregion
    }
}

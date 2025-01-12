using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler, 
        IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationResponse>>,
        IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        #region fields

        #endregion
        #region ctor
        public UserQueryHandler(IMapper mapper,IStringLocalizer<SharedResources> stringLocalizer,UserManager<User> userManager):base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }

        #endregion
        #region functions
        public  Task<PaginatedResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            //
            var users =  _userManager.Users.AsQueryable();
            var paginatedList = _mapper
                .ProjectTo<GetUserPaginationResponse>(users).ToPaginatedListAsync(request.PageNumber,request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
           var user=await  _userManager.Users.FirstOrDefaultAsync(x=>x.Id == request.Id);
           //var user1 =_userManager.FindByIdAsync(request.Id.ToString());
           if (user == null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
           var result= _mapper.Map<GetUserByIdResponse>(user);
            return Success( result);
        }
        #endregion

    }
}

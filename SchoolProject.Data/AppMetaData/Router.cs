﻿using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.AppMetaData
{
    public static class Router
    {
        public const string singleRoute = "/{id}";

        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";

        public static class StudentRouting
        {
            public const string Prefix = Rule + "Student";
            public const string List = Prefix + "/list";
            public const string GetByID = Prefix + singleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + singleRoute;
            public const string Paginted = Prefix + "/Paginted";


        }
        public static class DepartmentRouting
        {
            public const string Prefix = Rule + "Department";
            public const string GetByID = Prefix + "/Id";
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + singleRoute;

        }
        public static class ApplicationUserRouting
        {
            public const string Prefix = Rule + "User";
            public const string GetByID = Prefix + singleRoute;
            public const string Create = Prefix + "/Create";
            public const string Edit = Prefix + "/Edit";
            public const string Delete = Prefix + singleRoute;
            public const string Paginted = Prefix + "/Paginted";
            public const string ChangePassword = Prefix + "/Change-Password";

        }
        public static class Authentication
        {
            public const string Prefix = Rule + "Authentication";   
            public const string SignIn = Prefix + "/SignIn";
            public const string RefreshToken = Prefix + "/RefreshToken";
            public const string Delete = Prefix + singleRoute;
            public const string Paginted = Prefix + "/Paginted";
            public const string ValidateToken = Prefix + "/ValidateToken";

        }
        public static class AuthorizationRouting
        {
            public const string Prefix = Rule + "AuthorizationRouting";
            public const string Roles = Prefix + "/Role";
            public const string Claims = Prefix + "/Claims";
            public const string Create = Roles + "/Create";
            public const string RoleList = Roles + "/Role-List";
            public const string Edit = Roles + "/Edit";
            public const string Delete = Roles + "/Delete/{id}";
            public const string GetRoleById = Roles + "/RoleById/{id}";
            public const string ManageUserRoles = Roles + "/ManageUserRoles/{userId}";
            public const string UpdateUserRoles = Roles + "/Update-User-Roles";
            public const string ManageUserClaims = Claims + "/Manage-User-Claims/{userId}";
        }

    }
}


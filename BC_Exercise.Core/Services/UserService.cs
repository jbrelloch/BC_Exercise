﻿using System;
using System.Collections.Generic;
using System.Linq;
using BC_Exercise.Core.Interfaces;
using BC_Exercise.Models.User;
using Raven.Client;

namespace BC_Exercise.Core.Services
{
    public class UserService : IUserService
    {
        private IExternalService ExternalService;

        public UserService(IExternalService externalService)
        {
            ExternalService = externalService;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="ravenSession">The raven session.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The user.</returns>
        public User GetUser(IDocumentSession ravenSession, string id)
        {
            var result = ravenSession.Load<User>(id);
            
            return result;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="ravenSession">The raven session.</param>
        /// <returns>The users.</returns>
        public List<User> GetAllUsers(IDocumentSession ravenSession)
        {
            var result = ravenSession.Query<User>().Customize(x => x.WaitForNonStaleResults()).ToList();

            return result;
        }

        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="ravenSession">The raven session.</param>
        /// <param name="model">The model.</param>
        /// <returns>The identifier of the added user.</returns>
        public string AddUser(IDocumentSession ravenSession, User model)
        {
            if (!String.IsNullOrEmpty(model.StreetAddress)
                && !String.IsNullOrEmpty(model.City)
                && !String.IsNullOrEmpty(model.State)
                && !String.IsNullOrEmpty(model.Zip))
            {
                var addrStr = ExternalService.GetAddressStrFromObject(model);
                model.Coordinates = ExternalService.GetCoordinatesFromAddress(addrStr);
            }

            ravenSession.Store(model);
            ravenSession.SaveChanges();

            return model.Id;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="ravenSession">The raven session.</param>
        /// <param name="model">The model.</param>
        /// <returns>The updated user.</returns>
        public User UpdateUser(IDocumentSession ravenSession, User model)
        {
            var currentDbModel = ravenSession.Load<User>(model.Id);

            if (currentDbModel == null)
            {
                return null;
            }

            currentDbModel.FirstName = model.FirstName;
            currentDbModel.LastName = model.LastName;
            currentDbModel.Email = model.Email;
            
            if (currentDbModel.StreetAddress != model.StreetAddress
                || currentDbModel.City != model.City
                || currentDbModel.State != model.State
                || currentDbModel.Zip != model.Zip)
            {
                var addrStr = ExternalService.GetAddressStrFromObject(model);
                currentDbModel.Coordinates = ExternalService.GetCoordinatesFromAddress(addrStr);
            }

            currentDbModel.StreetAddress = model.StreetAddress;
            currentDbModel.City = model.City;
            currentDbModel.State = model.State;
            currentDbModel.Zip = model.Zip;
            
            ravenSession.SaveChanges();

            return currentDbModel;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="ravenSession">The raven session.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>The identifier of the deleted user.</returns>
        public string DeleteUser(IDocumentSession ravenSession, string id)
        {
            var currentDbModel = ravenSession.Load<User>(id);

            if (currentDbModel == null)
            {
                return null;
            }

            ravenSession.Delete<User>(currentDbModel);
            ravenSession.SaveChanges();

            return id;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TopCatAuthentication.Services
{
    public class WebAuthenticationService :IWebAuthenticationService
    {
        // NOTE: THIS HAS TO BE UPDATED TO MATCH YOUR SITE/EXAMPLE and sites must be https
        private string Origin = "https://localhost:44340";
        private readonly U2FContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<WebAuthenticationService> _logger;

        public WebAuthenticationService(U2FContext userRepository,
            UserManager<User> userManager,
            ILogger<WebAuthenticationService> logger)
        {
            Origin = "";
            _dataContext = userRepository;
            _userManager = userManager;
            _logger = logger;
        }

        #region IWebAuthenticationService methods

        public async Task<bool> SaveNewUser(string userName, string password, string email)
        {
            bool result = await IsUserRegistered(userName);
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email) || result)
                return false;
            /*
            User newUser = new User { Email = email.Trim(), CreatedOn = DateTime.Now, UserName = userName.Trim() };
            IdentityResult userCreatedResult = await _userManager.CreateAsync(newUser, password.Trim());

            if (userCreatedResult.Succeeded)
            {
                _logger.LogInformation($"user {newUser.UserName} was created email {newUser.Email}");
                return true;
            }
            else
            {
                _logger.LogInformation($"failed to create user {userName}");
                foreach (IdentityError error in userCreatedResult.Errors)
                {
                    _logger.LogError(error.Description);
                }
                return false;
            }
            */
            return true;
        }

        public async Task<ServerRegisterResponse> GenerateServerChallenge(string username)
        {
            User user = await FindUserByUsername(username);
            if (user == null)
                return null;

            StartedRegistration startedRegistration = Core.Crypto.U2F.StartRegistration(Origin);

            if (user.AuthenticationRequest == null)
                user.AuthenticationRequest = new List<AuthenticationRequest>();
            if (user.DeviceRegistrations == null)
                user.DeviceRegistrations = new List<Device>();

            user.AuthenticationRequest.Add(
             new AuthenticationRequest
             {
                 AppId = startedRegistration.AppId,
                 Challenge = startedRegistration.Challenge,
                 Version = Core.Crypto.U2F.U2FVersion
             });

            user.UpdatedOn = DateTime.Now;

            await _dataContext.SaveChangesAsync();

            return new ServerRegisterResponse
            {
                AppId = startedRegistration.AppId,
                Challenge = startedRegistration.Challenge,
                Version = startedRegistration.Version
            };
        }

        public async Task<bool> CompleteRegistration(string userName, string deviceResponse)
        {
            if (string.IsNullOrWhiteSpace(deviceResponse))
                return false;
            /*
            User user = await FindUserByUsername(userName);

            if (user?.AuthenticationRequest == null || user.AuthenticationRequest.Count == 0)
                return false;


            RegisterResponse registerResponse = RegisterResponse.FromJson<RegisterResponse>(deviceResponse);

            // TODO When the user is registration they should only ever have one auth request.????
            AuthenticationRequest authenticationRequest = user.AuthenticationRequest.First();

            StartedRegistration startedRegistration = new StartedRegistration(authenticationRequest.Challenge, authenticationRequest.AppId);
            DeviceRegistration registration = Core.Crypto.U2F.FinishRegistration(startedRegistration, registerResponse);

            user.AuthenticationRequest.Clear();
            user.UpdatedOn = DateTime.Now;
            user.DeviceRegistrations.Add(new Device
            {
                AttestationCert = registration.AttestationCert,
                Counter = Convert.ToInt32(registration.Counter),
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,
                KeyHandle = registration.KeyHandle,
                PublicKey = registration.PublicKey
            });
            */
            int result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AuthenticateUser(string userName, string deviceResponse)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(deviceResponse))
                return false;
            /*
            User user = await FindUserByUsername(userName);
            if (user == null)
                return false;

            AuthenticateResponse authenticateResponse = AuthenticateResponse.FromJson<AuthenticateResponse>(deviceResponse);

            Device device = user.DeviceRegistrations.FirstOrDefault(f => f.KeyHandle.SequenceEqual(authenticateResponse.KeyHandle.Base64StringToByteArray()));

            if (device == null || user.AuthenticationRequest == null)
                return false;

            // User will have a authentication request for each device they have registered so get the one that matches the device key handle
            AuthenticationRequest authenticationRequest = user.AuthenticationRequest.First(f => f.KeyHandle.Equals(authenticateResponse.KeyHandle));
            DeviceRegistration registration = new DeviceRegistration(device.KeyHandle, device.PublicKey, device.AttestationCert, Convert.ToUInt32(device.Counter));

            StartedAuthentication authentication = new StartedAuthentication(authenticationRequest.Challenge, authenticationRequest.AppId, authenticationRequest.KeyHandle);

            Core.Crypto.U2F.FinishAuthentication(authentication, authenticateResponse, registration);

            user.AuthenticationRequest.Clear();
            user.UpdatedOn = DateTime.Now;

            device.Counter = Convert.ToInt32(registration.Counter);
            device.UpdatedOn = DateTime.Now;
            */
            int result = await _dataContext.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> IsUserRegistered(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return false;

            User user = await FindUserByUsername(userName);

            return user != null;
        }

        public async Task<List<ServerChallenge>> GenerateServerChallenges(string userName)
        {
            User user = await FindUserByUsername(userName);

            if (user == null)
                return null;

            // We only want to generate challenges for un-compromised devices
            List<Device> devices = user.DeviceRegistrations.Where(w => w.IsCompromised == false).ToList();

            if (devices.Count == 0)
                return null;

            user.AuthenticationRequest.Clear();

            List<ServerChallenge> serverChallenges = new List<ServerChallenge>();
            foreach (Device registeredDevice in devices)
            {
                DeviceRegistration registration = new DeviceRegistration(registeredDevice.KeyHandle, registeredDevice.PublicKey, registeredDevice.AttestationCert, Convert.ToUInt32(registeredDevice.Counter));
                StartedAuthentication startedAuthentication = Core.Crypto.U2F.StartAuthentication(Origin, registration);

                serverChallenges.Add(new ServerChallenge
                {
                    appId = startedAuthentication.AppId,
                    challenge = startedAuthentication.Challenge,
                    keyHandle = startedAuthentication.KeyHandle,
                    version = startedAuthentication.Version
                });
                user.AuthenticationRequest.Add(
                new AuthenticationRequest
                {
                    AppId = startedAuthentication.AppId,
                    Challenge = startedAuthentication.Challenge,
                    KeyHandle = startedAuthentication.KeyHandle,
                    Version = startedAuthentication.Version
                });
            }
            user.UpdatedOn = DateTime.Now;
            await _dataContext.SaveChangesAsync();
            return serverChallenges;
        }


        public async Task<User> FindUserByUsername(string username)
        {
            User user = null;

            if (!string.IsNullOrWhiteSpace(username))
            {
                if (await _dataContext.Users.AnyAsync(person => person.UserName == username.Trim()))
                {
                    user = await _dataContext.Users
                        .Include(i => i.AuthenticationRequest)
                        .Include(i => i.DeviceRegistrations)
                        .FirstAsync(person => person.UserName == username.Trim());
                }
            }

            return user;
        }

        #endregion
    }
}

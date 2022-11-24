using System;

namespace CommandsRegistry.WebUI.DTO.Authentication
{
    public class ResetPasswordRequest
    {
        public Guid UserId { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
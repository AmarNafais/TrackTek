using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace Services;

public interface IUserService
{
    void CreateUser(User user);
    object GetUserById(int id);
    object GetAllUsers();
    void UpdateUser(User user);
    User GetUserByEmailAndPassword(string email, string password);
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IEmailService emailService, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordHasher = passwordHasher;
    }

    public void CreateUser(User user)
    {
        var generatedPassword = GenerateRandomPassword(8);
        var newUser = new User()
        {
            Name = user.Name,
            Email = user.Email,
            Password = _passwordHasher.HashPassword(user, generatedPassword),
        };
        _emailService.SendWelcomeEmail(user, generatedPassword);
        _userRepository.AddUser(newUser);
    }

    public object GetUserById(int id)
    {
        var user = _userRepository.GetByUserId(id);
        return new
        {
            user.Id,
            user.Name,
            user.Email,
        };
    }

    public object GetAllUsers()
    {
        var users = _userRepository.GetAllUsers();
        return users.Select(user => new
        {
            user.Id,
            user.Name,
            user.Email,
        });
    }

    public void UpdateUser(User user)
    {
        var updateUser = new User()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        };

        _userRepository.UpdateUser(updateUser);
    }

    public User GetUserByEmailAndPassword(string email, string password)
    {
        var user = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email == email);

        if (user == null)
            return null;

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (passwordVerificationResult == PasswordVerificationResult.Success)
            return user;

        return null;
    }

    private string GenerateRandomPassword(int length)
    {
        const string validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var password = new StringBuilder();
        using (var rng = RandomNumberGenerator.Create())
        {
            var byteArray = new byte[1];

            for (int i = 0; i < length; i++)
            {
                rng.GetBytes(byteArray);
                var randomIndex = byteArray[0] % validCharacters.Length;
                password.Append(validCharacters[randomIndex]);
            }
        }
        return password.ToString();
    }
}
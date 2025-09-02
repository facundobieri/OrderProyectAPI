using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();
            return users.Select(user => MapToDto(user));
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");

            return MapToDto(user);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con email {email} no encontrado");
            return MapToDto(user);
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            // Validar que el email no exista ya
            var existingUser = await _userRepository.GetByEmail(createUserDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el email {createUserDto.Email}");

            // Crear la entidad de usuario
            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                Password = HashPassword(createUserDto.Password),
                Role = ParseRole(createUserDto.Role),
                IsActive = true // Asegurar que el usuario esté activo al crearlo
            };

            // Guardar en la base de datos
            var createdUser = await _userRepository.CreateUser(user);
            return MapToDto(createdUser);
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado");

            // Actualizar propiedades no nulas
            if (updateUserDto.Username != null)
                user.Username = updateUserDto.Username;

            if (updateUserDto.Email != null)
            {
                // Verificar que el email no esté en uso
                var existingUser = await _userRepository.GetByEmail(updateUserDto.Email);
                if (existingUser != null && existingUser.Id != id)
                    throw new InvalidOperationException($"Ya existe un usuario con el email {updateUserDto.Email}");

                user.Email = updateUserDto.Email;
            }

            if (updateUserDto.Password != null)
                user.Password = HashPassword(updateUserDto.Password);

            if (updateUserDto.Role != null)
                user.Role = ParseRole(updateUserDto.Role);

            await _userRepository.UpdateUser(user);
            return MapToDto(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
                return false;

            // Ahora esto implementará una baja lógica en lugar de física
            await _userRepository.DeleteUser(id);
            return true;
        }

        // Métodos auxiliares
        private UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role.ToString(),
                IsActive = user.IsActive
            };
        }

        private string HashPassword(string password)
        {
            // En producción, usar BCrypt o similar
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }

        private UserRole ParseRole(string role)
        {
            if (Enum.TryParse<UserRole>(role, true, out var userRole))
                return userRole;
            
            return UserRole.Client; // Rol predeterminado
        }

        
    }
}

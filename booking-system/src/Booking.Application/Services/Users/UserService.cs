using AutoMapper;
using AutoMapper.QueryableExtensions;
using Booking.Application.Common.Jwt.Enums;
using Booking.Application.Common.Jwt.Models;
using Booking.Application.Common.Pagination;
using Booking.Application.Exceptions;
using Booking.Application.Interfaces.Repositories.Users;
using Booking.Application.Interfaces.Services.Token;
using Booking.Application.Interfaces.Services.Users;
using Booking.Application.Models.Constants;
using Booking.Application.Models.DTOs.Request.Users;
using Booking.Application.Models.DTOs.Response.Users;
using Booking.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<PaginatedList<UserResponse>> GetAll(QueryStringParameters queryParams)
        {
            var query = _userRepository.TableNoTracking;

            var entity = query.ProjectTo<UserResponse>(_mapper.ConfigurationProvider);
            var result = new PaginatedList<UserResponse>(queryParams);
            await result.CreateAsync(entity);
            return result;
        }

        public async Task<UserResponse> Create(UserRequest request)
        {
            var item = _userRepository.Table.FirstOrDefault(x => x.PhoneNumber == request.PhoneNumber);

            if (item != null) // throw new BadRequestException(ExceptionCodes.DublicateData, "Phone has already been taken!");
            {
                return new UserResponse { Id = item.Id };
            }
            var entity = _mapper.Map<User>(request);
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password);
            _userRepository.Add(entity);

            return new UserResponse { Id = entity.Id };
        }

        public async Task<UserResponse> GetById(int id)
        {
            var item = await _userRepository.TableNoTracking.Include(x => x.Role)
                                                            .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            return _mapper.Map<UserResponse>(item);
        }

        public async Task Update(UserRequest request)
        {
            var entity = _mapper.Map<User>(request);

            var entityDb = await _userRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entityDb == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            await _userRepository.UpdateAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entityDb = await _userRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

            if (entityDb == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            await _userRepository.DeleteAsync(entityDb);
        }

        public async Task<GetAccessTokenResponse> GetAccessToken(GetAccessTokenRequest request)
        {
            var item = await _userRepository.TableNoTracking.FirstOrDefaultAsync(x => x.PhoneNumber == request.UserName &&
                                                                                      x.IsActive == true);

            if (item == null)
                throw new BadRequestException(ExceptionCodes.InvalidCredentials, "Username or password incorrect!");

            bool passwordCheck = BCrypt.Net.BCrypt.Verify(request.Password, item.Password);
            if (!passwordCheck)
                throw new BadRequestException(ExceptionCodes.InvalidCredentials, "Username or password incorrect!");

            var token = await _tokenService.GenerateToken(new GenerateTokenRequest
            {
                UserId = item.Id,
                RoleId = item.RoleId,
                TokenType = TokenType.Access
            });
            return new GetAccessTokenResponse
            {
                UserId = item.Id,
                AccessToken =token.Token,
                IsProvider=token.IsProvider
            };
        }
    }
}

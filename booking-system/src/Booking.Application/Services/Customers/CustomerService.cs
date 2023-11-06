using AutoMapper;
using Booking.Application.Common.Pagination;
using Booking.Application.Exceptions;
using Booking.Application.Interfaces.Repositories.Customers;
using Booking.Application.Interfaces.Repositories.Dictionaries;
using Booking.Application.Interfaces.Services.Customers;
using Booking.Application.Interfaces.Services.Users;
using Booking.Application.Interfaces.Shared;
using Booking.Application.Models.Constants;
using Booking.Application.Models.DTOs.Files;
using Booking.Application.Models.DTOs.Request.Customers;
using Booking.Application.Models.DTOs.Response.Customers;
using Booking.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IGenderTranslationRepository _genderTranslationRepository;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly IUserService _userService;
        public CustomerService(ICustomerRepository CustomerRepository, IMapper mapper, IFileStorageService fileStorageService, IGenderRepository genderRepository, 
                               IGenderTranslationRepository genderTranslationRepository, IUserService userService)
        {
            _customerRepository = CustomerRepository;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _genderRepository = genderRepository;
            _genderTranslationRepository = genderTranslationRepository;
            _userService = userService;
        }

        public async Task<PaginatedList<CustomerResponse>> GetAll(QueryStringParameters queryParams, string lang)
        {
            var query = (from wp in _customerRepository.TableNoTracking
                         join gen in _genderRepository.TableNoTracking on wp.GenderId equals gen.Id
                         join genTrans in _genderTranslationRepository.TableNoTracking on gen.Id equals genTrans.GenderId
                         where genTrans.Language.ShortName == lang
                            && wp.IsActive == true
                         select new CustomerResponse
                         {
                             Id = wp.Id,
                             IsActive = wp.IsActive,
                             ProfileImageUrl = wp.ProfileImageUrl,
                             Name = wp.Name,
                             Surname = wp.Surname,
                             MiddleName = wp.MiddleName,
                             BirthDate = wp.BirthDate,
                             Phone = wp.Phone,
                             Email = wp.Email,
                             GenderId = wp.GenderId,
                             GenderName = genTrans.Name,
                             Description = wp.Description
                         });
            var result = new PaginatedList<CustomerResponse>(queryParams);
            await result.CreateAsync(query);
            foreach (var item in result.Items)
            {
                item.Image = await _fileStorageService.Download(item.ProfileImageUrl);
            }
            return result;
        }

        public async Task<CustomerResponse> GetById(int id)
        {
            var item = await _customerRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            var entity = _mapper.Map<CustomerResponse>(item);
            entity.Image = await _fileStorageService.Download(item.ProfileImageUrl);
            return entity;
        }

        public async Task<int> Create(CustomerRequest request)
        {
             var customer = await _customerRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Phone == request.Phone);
            if (customer != null)
                return customer.Id;

            var entity = _mapper.Map<Customer>(request);
            entity.User.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _customerRepository.AddAsync(entity);
            entity.ProfileImageUrl = await _fileStorageService.Upload(new FileRequest
            {
                FilePath = $@"Customers\{entity.Id}",
                File = request.Image
            });
            await _customerRepository.Commit();
            return entity.Id;
        }
        public async Task Update(CustomerRequest request)
        {
            var entity = _mapper.Map<Customer>(request);
            var item = await _customerRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == request.Id);
            entity.ProfileImageUrl = item.ProfileImageUrl;
            await _customerRepository.UpdateAsync(entity);
        }
        public async Task Delete(int id)
        {
            var item = await _customerRepository.Table.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            await _customerRepository.DeleteAsync(item);
        }

        public async Task ChangeImage(ImageRequest request)
        {
            var entity = await _customerRepository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
                throw new NotFoundException(ExceptionCodes.NotFound, "Data not found!");

            await _fileStorageService.Delete(entity.ProfileImageUrl);

            entity.ProfileImageUrl = await _fileStorageService.Upload(new FileRequest
            {
                FilePath = $@"Customers\{entity.Id}",
                File = request.Image
            });
            await _customerRepository.UpdateAsync(entity);
        }
    }
}

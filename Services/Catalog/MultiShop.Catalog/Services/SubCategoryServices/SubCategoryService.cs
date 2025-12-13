using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.SubCategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.SubCategoryServices
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IMongoCollection<SubCategory> _subCategoryCollection;
        private readonly IMapper _mapper;

        public SubCategoryService(IDatabaseSettings _databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _subCategoryCollection = database.GetCollection<SubCategory>(_databaseSettings.SubCategoryCollectionName);
            _mapper = mapper;
        }

        public async Task ChangeSubCategoryStatus(string id)
        {
            var subCategory = await _subCategoryCollection.Find(x => x.SubCategoryId == id).FirstOrDefaultAsync();

            if (subCategory != null)
            {
                subCategory.SubCategoryStatus = !subCategory.SubCategoryStatus;
                await _subCategoryCollection.FindOneAndReplaceAsync(x => x.SubCategoryId == id, subCategory);
            }
        }

        public async Task CreateSubCategoryAsync(CreateSubCategoryDto createSubCategoryDto)
        {
            var value = _mapper.Map<SubCategory>(createSubCategoryDto);
            value.SubCategoryStatus = true;
            await _subCategoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteSubCategoryAsync(string id)
        {
            await _subCategoryCollection.DeleteOneAsync(x => x.SubCategoryId == id);
        }

        public async Task<List<ResultSubCategoryDto>> GetAllSubCategoryAsync()
        {
            var values = await _subCategoryCollection.Find(x => true).ToListAsync();
            return _mapper.Map<List<ResultSubCategoryDto>>(values);
        }

        public async Task<GetByIdSubCategoryDto> GetByIdSubCategoryAsync(string id)
        {
            var value = await _subCategoryCollection.Find<SubCategory>(x => x.SubCategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdSubCategoryDto>(value);
        }

        public async Task<List<ResultSubCategoryDto>> GetSubCategoryByCategoryIdAsync(string categoryId)
        {
            var values = await _subCategoryCollection.Find(x => x.CategoryId == categoryId).ToListAsync();
            return _mapper.Map<List<ResultSubCategoryDto>>(values);
        }

        public async Task UpdateSubCategoryAsync(UpdateSubCategoryDto updateSubCategoryDto)
        {
            var value = _mapper.Map<SubCategory>(updateSubCategoryDto);
            await _subCategoryCollection.FindOneAndReplaceAsync(x => x.SubCategoryId == updateSubCategoryDto.SubCategoryId, value);
        }
    }
}

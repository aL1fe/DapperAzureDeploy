using DapperLearning.ModelsDto;

namespace DapperLearning.Contracts;

public interface IRaceRepository
{
    // Get
    public Task<IEnumerable<RaceBasicInfoDto>>? GetRacesDescription();
    public Task<IEnumerable<RaceFullInfoDto>>? GetRacesFullInfo();
    public Task<RaceBasicInfoDto>? GetRaceByName(string name);
    
    // Post
    public Task AddNewRace(RaceFullInfoDto newRace);
    
    //Update
    
    //Delete
    public Task DeleteRaceById(Guid id);
}

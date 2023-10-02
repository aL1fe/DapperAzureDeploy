using System.Data;
using Dapper;
using DapperLearning.Contracts;
using DapperLearning.Data;
using DapperLearning.ModelsDto;

namespace DapperLearning.Services;

public class RaceRepository: IRaceRepository
{
    private readonly DapperContext _context;
    public RaceRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RaceBasicInfoDto>> GetRacesDescription()
    {
        var query = $@"SELECT 
                          RaceName as {nameof(RaceBasicInfoDto.RaceName)}
                          ,Description as {nameof(RaceBasicInfoDto.Description)}
                       FROM dbo.Races ";
        using var connection = _context.CreateConnection();
        var races = await connection.QueryAsync<RaceBasicInfoDto>(query);
        return races.ToList();
    }

    public async Task<IEnumerable<RaceFullInfoDto>> GetRacesFullInfo()
    {
        var query = $@"SELECT 
	                    R.RaceName,
	                    F.FactionName,
	                    R.Description,
	                    R.Leader,
	                    R.SpecialAbilities
                    FROM dbo.Races R
                    JOIN dbo.Factions F ON F.Id = R.FactionId ";
        using var connection = _context.CreateConnection();
        var races = await connection.QueryAsync<RaceFullInfoDto>(query);
        return races.ToList();
    }

    public async Task<RaceBasicInfoDto> GetRaceByName(string name)
    {
        var query = $@"SELECT 
                        * 
                    FROM dbo.Races 
                    WHERE RaceName = @Name";
        using var connection = _context.CreateConnection();
        var race = await connection.QuerySingleOrDefaultAsync<RaceBasicInfoDto>(query, new { name });
        return race;
    }
    
    public async Task AddNewRace(RaceFullInfoDto newRace)
    {
        var parameters = new DynamicParameters();
        parameters.Add("RaceName", newRace.RaceName, DbType.String);
        parameters.Add("Faction", newRace.FactionName, DbType.String);
        parameters.Add("Description", newRace.Description, DbType.String);
        parameters.Add("Leader", newRace.Leader, DbType.String);
        parameters.Add("SpecialAbilities", newRace.SpecialAbilities, DbType.String);
        
        var query = $@"INSERT INTO dbo.Races 
                        (
                        RaceName, 
                        FactionId, 
                        Description, 
                        Leader, 
                        SpecialAbilities
                        )
                    SELECT 
                        @RaceName, 
                        (SELECT Id FROM Factions WHERE FactionName = @Faction), 
                        @Description,
                        @Leader,
                        @SpecialAbilities";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
    }

    public async Task DeleteRaceById(Guid id)
    {
        var query = "DELETE dbo.Races WHERE Id = @Id";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { id });
    }
}
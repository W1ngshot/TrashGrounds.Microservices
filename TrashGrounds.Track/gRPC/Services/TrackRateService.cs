using TrackRateClient;
using TrashGrounds.Track.Models.Main;

namespace TrashGrounds.Track.gRPC.Services;

public class TrackRateService
{
    private readonly TrackRateClient.TrackRateService.TrackRateServiceClient _client;

    public TrackRateService(TrackRateClient.TrackRateService.TrackRateServiceClient client)
    {
        _client = client;
    }

    public async Task<Rate?> GetTrackRate(Guid id)
    {
        try
        {
            return new Rate(
                id,
                (await _client.GetTrackRateAsync(new GetTrackRateRequest {Id = id.ToString()})).Rating);
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Rate>?> GetTracksRate(IEnumerable<Guid> ids)
    {
        try
        {
            var request = new GetTracksRateRequest();
            request.Ids.Add(ids.Select(id => id.ToString()));

            var response = await _client.GetTracksRateAsync(request);

            return response.Rates.Select(rate => new Rate(Guid.Parse(rate.Id), rate.Rating));
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Rate>?> GetBestRatedTracks(int take, int skip)
    {
        try
        {
            var response =
                await _client.GetBestRatedTrackAsync(new GetBestRatedTracksRequest {Count = take, Skip = skip});

            return response.Rates.Select(rate => new Rate(Guid.Parse(rate.Id), rate.Rating));
        }
        catch (Exception e)
        {
            return null;
        }
    }
}
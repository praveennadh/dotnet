using FastEndpoints;
using fleetsystem.repositories;
using fleetsystem.dtos;
using fleetsystem.entities;

public class CreateDriverEndpoint : Endpoint<DriverRequest, DriverResponse>
{
    private readonly IDriverRepository _repository;

    public CreateDriverEndpoint(IDriverRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/drivers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DriverRequest request, CancellationToken ct)
    {
        // Create a new driver entity from the request data
        var newDriver = new Driver(request.Name, request.License, request.Details);
        // {
        //     Name = request.Name,
        //     License = request.License,
        //     Details = request.Details
        // };

        // Add the new driver using the repository
        var createdDriver = await _repository.AddDriverAsync(newDriver);

        // Return the created driver as a DriverResponse DTO
        var response = new DriverResponse
        {
            Id = createdDriver.Id,
            Name = createdDriver.Name,
            License = createdDriver.License,
            Details = createdDriver.Details
        };

        // Send the response
        await SendAsync(response);
    }
}

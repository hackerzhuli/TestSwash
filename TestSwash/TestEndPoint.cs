using Microsoft.AspNetCore.Http.HttpResults;

namespace TestSwash;

public record What(string Content);

public static class TestEndPoint
{
    /// <summary>
    /// This is swash
    /// </summary>
    /// <param name="name" example="my name">The name</param>
    /// <returns></returns>
    public static Ok<What> Swash(string name)
    {
        return TypedResults.Ok(new What("what"));
    } 
}


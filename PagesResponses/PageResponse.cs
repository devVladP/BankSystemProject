using System.ComponentModel.DataAnnotations;

namespace PagesResponses;

public record PageResponse<T>([property: Required] int Total, [property: Required] T Results) where T : class;

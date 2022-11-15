using System;
using System.Collections.Generic;

namespace CCDRS.Model;

public partial class SurveyStation
{
    public int Id { get; set; }

    public int StationId { get; set; }

    public int SurveyId { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual Survey Survey { get; set; } = null!;
}

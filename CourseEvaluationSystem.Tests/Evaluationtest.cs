using CourseEvaluationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class EvaluationTests
{
    [Fact]
    public void Evaluation_IsInvalid_IfRatingOutOfRange()
    {
        var evaluation = new Evaluation { Rating = 0 };
        Assert.False(evaluation.Rating >= 1 && evaluation.Rating <= 5);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(3)]
    [InlineData(5)]
    public void Evaluation_IsValid_IfRatingBetween1And5(int rating)
    {
        var evaluation = new Evaluation { Rating = rating };
        Assert.True(evaluation.Rating >= 1 && evaluation.Rating <= 5);
    }

    [Fact]
    public void CalculateAverageRating_ReturnsExpectedAverage()
    {
        var evaluations = new List<Evaluation>
        {
            new Evaluation { Rating = 4 },
            new Evaluation { Rating = 2 },
            new Evaluation { Rating = 5 }
        };

        var average = evaluations.Average(e => e.Rating);
        Assert.Equal(3.67, average, 2);
    }

    [Fact]
    public void CalculateAverageRating_ReturnsZero_WhenNoEvaluationsExist()
    {
        var evaluations = new List<Evaluation>();
        var average = evaluations.Any()
            ? evaluations.Average(e => e.Rating)
            : 0;

        Assert.Equal(0, average);
    }
}

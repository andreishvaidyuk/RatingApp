namespace RatingApp
{
    public static class RatingCalculation
    {
        // метод расчета изменения рейтинга по итогам встреч
        public static double ToCalculateDelta(object firstPlayerRating, object secondPlayerRating, bool isWin)
        {
            double delta;
            if (isWin)
            {
                delta = (100 - ((double)firstPlayerRating - (double)secondPlayerRating)) / 10;
            }
            else
            {
                delta = (100 - ((double)secondPlayerRating - (double)firstPlayerRating)) / -20;
            }
            return delta;
        }

        //метод добавления к изменению рейтинга по итогам встреч бонусов и учета коэффициентов за уровень соревнования
        public static double ToCalculateTotalDelta(double averageDeltaCompetition, int bonusForPlace, double kzs, double leagueCoefficient)
        {
            double totalDelta = 0;
            totalDelta += (averageDeltaCompetition + bonusForPlace) * kzs * leagueCoefficient;
            return totalDelta;
        }
    }
}

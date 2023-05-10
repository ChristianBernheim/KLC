using KLC.Models;

namespace KLC.Controllers
{
	public class Util
	{
		//calc for action
		private static int sum = 0;
		private static bool threeflag = false;

		public static int GetNEWSAction(MatningNews2 matning)
		{
			//reset values
			sum = 0;
			threeflag = false;

			CheckValue(matning.Andningsfrekvens);
			CheckValue(matning.SyreMattnad);
			CheckValue(matning.TillfordSyrgas);
			CheckValue(matning.SystolisktBlodtryck);
			CheckValue(matning.Pulsfrekvens);
			CheckValue(matning.Medvetandegrad);
			CheckValue(matning.Temperatur);


				if (sum >= 7)
				{
					return 4;
				}
				else if (sum == 6 || sum == 5)
				{
					return 3;
				}
				else if (threeflag == true)
				{
					return 2;
				}
				else if (sum <= 4 && sum >= 1)
				{
					return 1;
				}
				else
				{
					return 0;
				}

			}


		private static void CheckValue(int? number)
		{
			if (number != null)
			{
				if (number >= 7)
				{
					number = number - 10;
				}

				int absnumber = Math.Abs((int)number);

				if (absnumber == 3)
				{
					threeflag = true;
				}
				sum += absnumber;
				Console.WriteLine(absnumber);
			}
			

		}

	}
}

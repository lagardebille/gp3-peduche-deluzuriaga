public class FastestTime {

	public static int[] times_normal = new int[24];
	public static int[] times_hard = new int[24];

	public static int chapter_1_normal = 0;
	public static int chapter_2_normal = 0;
	public static int chapter_3_normal = 0;
	public static int chapter_4_normal = 0;
	public static int chapter_5_normal = 0;
	public static int chapter_6_normal = 0;

	public static int chapter_1_hard = 0;
	public static int chapter_2_hard = 0;
	public static int chapter_3_hard = 0;
	public static int chapter_4_hard = 0;
	public static int chapter_5_hard = 0;
	public static int chapter_6_hard = 0;

	public FastestTime() {

	}

	public static int GetTimes(int i, int j) {
		TotalIt ();
		if (j == 2) {
			switch (i) {
			case 1:
					return chapter_1_normal;
					break;
				case 2:
					return chapter_2_normal;
					break;
				case 3:
					return chapter_3_normal;
					break;
				case 4:
					return chapter_4_normal;
					break;
				case 5:
					return chapter_5_normal;
					break;
				case 6: 
					return chapter_6_normal;
					break;
			}
		} else if (j == 3) {
			switch (i) {
				case 1:
					return chapter_1_hard;
					break;
				case 2:
					return chapter_2_hard;
					break;
				case 3:
					return chapter_3_hard;
					break;
				case 4:
					return chapter_4_hard;
					break;
				case 5:
					return chapter_5_hard;
					break;
				case 6: 
					return chapter_6_hard;
					break;
			}
		}
		return 0;
	}

	public static void TotalIt() {
		chapter_1_normal = times_normal [0] + times_normal [1] + times_normal [2] + times_normal [3];
		chapter_2_normal = times_normal [4] + times_normal [5] + times_normal [6] + times_normal [7];
		chapter_3_normal = times_normal [8] + times_normal [9] + times_normal [10] + times_normal [11];
		chapter_4_normal = times_normal [12] + times_normal [13] + times_normal [14] + times_normal [15];
		chapter_5_normal = times_normal [16] + times_normal [17] + times_normal [18] + times_normal [19];
		chapter_6_normal = times_normal [20] + times_normal [21] + times_normal [22] + times_normal [23];

		chapter_1_hard = times_hard [0] + times_hard [1] + times_hard [2] + times_hard [3];
		chapter_2_hard = times_hard [4] + times_hard [5] + times_hard [6] + times_hard [7];
		chapter_3_hard = times_hard [8] + times_hard [9] + times_hard [10] + times_hard [11];
		chapter_4_hard = times_hard [12] + times_hard [13] + times_hard [14] + times_hard [15];
		chapter_5_hard = times_hard [16] + times_hard [17] + times_hard [18] + times_hard [19];
		chapter_6_hard = times_hard [20] + times_hard [21] + times_hard [22] + times_hard [23];
	}
}
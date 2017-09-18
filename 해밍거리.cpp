#include<iostream>
#include<fstream>

using namespace std;

int main(void)
{
	ifstream fp;
	fp.open("input.txt");

	int test_case = 0, hammingnum1 = 0, hammingnum2 = 0;
	int i;
	fp >> test_case;
	for (i = 0; i < test_case; i++) {
		int j = 0, hamming_weight1 = 0, hamming_weight2 = 0, hamming_distance = 0;
		int bin1[32] = { 0, }, bin2[32] = { 0, };
		fp >> hammingnum1 >> hammingnum2;
		while (hammingnum1 != 0) {
			bin1[j] = hammingnum1 % 2;
			hammingnum1 /= 2;
			j++;
		}
		j = 0;
		while (hammingnum2 != 0) {
			bin2[j] = hammingnum2 % 2;
			hammingnum2 /= 2;
			j++;
		}
		for (j = 0; j < sizeof(bin1) / sizeof(int); j++) {
			if (bin1[j] == 1)hamming_weight1++;
			if (bin2[j] == 1)hamming_weight2++;
			if (bin1[j] != bin2[j])hamming_distance++;
		}
		cout <<hamming_weight1<<" "<<hamming_weight2<<" "<<hamming_distance<<endl;
	}
	return 0;
}
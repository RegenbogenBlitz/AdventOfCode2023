#include <stdio.h>

int part1(int *result)
{
   int total = 0;
   FILE *file = fopen("puzzle1input.txt", "r");

   if (file == NULL)
   {
      printf("Could not open file");
      return 1;
   }

   const int MAXLINELENGTH = 256;
   char line[MAXLINELENGTH];
   while (fgets(line, MAXLINELENGTH, file))
   {
      int first = -1;
      int last = -1;
      for (int i = 0; (line[i] != '\n' && i < MAXLINELENGTH); i++)
      {
         if ('0' <= line[i] && line[i] <= '9')
         {
            int digit = line[i] - '0';
            if (first == -1)
            {
               first = digit;
               last = digit;
            }
            else
            {
               last = digit;
            }
         }
      }

      int value = 10 * first + last;
      total += value;
   }

   fclose(file);
   *result = total;
   return 0;
}

int part2(int *result)
{
   int total = 0;
   FILE *file = fopen("example2.txt", "r");

   if (file == NULL)
   {
      printf("Could not open file");
      return 1;
   }

   const int MAXLINELENGTH = 256;
   char line[MAXLINELENGTH];
   while (fgets(line, MAXLINELENGTH, file))
   {
      int first = -1;
      int last = -1;
      int couldBe[] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

      for (int i = 0; (line[i] != '\n' && i < MAXLINELENGTH); i++)
      {
         int digit = -1;

         char character = line[i];

         if (character == 'o')
         {
            couldBe[1] = 1;
         }
         else if (character == 'n' && couldBe[1] == 1)
         {
            couldBe[1] = 2;
         }
         else if (character == 'e' && couldBe[1] == 2)
         {
            couldBe[1] = 0;
            digit = 1;
         }
         else
         {
            couldBe[1] = 0;
         }

         if (character == 't')
         {
            printf("t(2) ");
            couldBe[2] = 1;
         }
         else if (character == 'w' && couldBe[2] == 1)
         {
            printf("w(2) ");
            couldBe[2] = 2;
         }
         else if (character == 'o' && couldBe[2] == 2)
         {
            printf("o(2) ");
            couldBe[2] = 0;
            digit = 2;
         }
         else
         {
            couldBe[2] = 0;
         }

         if (character == 't')
         {
            couldBe[3] = 1;
         }
         else if (character == 'h' && couldBe[3] == 1)
         {
            couldBe[3]++;
         }
         else if (character == 'r' && couldBe[3] == 2)
         {
            couldBe[3]++;
         }
         else if (character == 'e' && couldBe[3] == 3)
         {
            couldBe[3]++;
         }
         else if (character == 'e' && couldBe[3] == 4)
         {
            couldBe[3] = 0;
            digit = 3;
         }
         else
         {
            couldBe[3] = 0;
         }

         if (character == 'f')
         {
            couldBe[4] = 1;
         }
         else if (character == 'o' && couldBe[4] == 1)
         {
            couldBe[4]++;
         }
         else if (character == 'u' && couldBe[4] == 2)
         {
            couldBe[4]++;
         }
         else if (character == 'r' && couldBe[4] == 3)
         {
            couldBe[4] = 0;
            digit = 4;
         }
         else
         {
            couldBe[4] = 0;
         }

         if (character == 'f')
         {
            couldBe[5] = 1;
         }
         else if (character == 'i' && couldBe[5] == 1)
         {
            couldBe[5]++;
         }
         else if (character == 'v' && couldBe[5] == 2)
         {
            couldBe[5]++;
         }
         else if (character == 'e' && couldBe[5] == 3)
         {
            couldBe[5] = 0;
            digit = 5;
         }
         else
         {
            couldBe[5] = 0;
         }

         if (character == 's')
         {
            printf("s(6) ");
            couldBe[6] = 1;
         }
         else if (character == 'i' && couldBe[6] == 1)
         {
            printf("i(6) ");
            couldBe[6]++;
         }
         else if (character == 'x' && couldBe[6] == 2)
         {
            printf("x(6) ");
            couldBe[6] = 0;
            digit = 6;
         }
         else
         {
            couldBe[6] = 0;
         }

         if (character == 's')
         {
            couldBe[7] = 1;
         }
         else if (character == 'e' && couldBe[7] == 1)
         {
            couldBe[7]++;
         }
         else if (character == 'v' && couldBe[7] == 2)
         {
            couldBe[7]++;
         }
         else if (character == 'e' && couldBe[7] == 3)
         {
            couldBe[7]++;
         }
         else if (character == 'n' && couldBe[7] == 4)
         {
            couldBe[7] = 0;
            digit = 7;
         }
         else
         {
            couldBe[7] = 0;
         }

         if (character == 'e')
         {
            couldBe[8] = 1;
         }
         else if (character == 'i' && couldBe[8] == 1)
         {
            couldBe[8]++;
         }
         else if (character == 'g' && couldBe[8] == 2)
         {
            couldBe[8]++;
         }
         else if (character == 'h' && couldBe[8] == 3)
         {
            couldBe[8]++;
         }
         else if (character == 't' && couldBe[8] == 4)
         {
            couldBe[8] = 0;
            digit = 8;
         }
         else
         {
            couldBe[8] = 0;
         }

         if (character == 'n' && couldBe[9] != 2)
         {
            couldBe[9] = 1;
         }
         else if (character == 'i' && couldBe[9] == 1)
         {
            couldBe[9]++;
         }
         else if (character == 'n' && couldBe[9] == 2)
         {
            couldBe[9]++;
         }
         else if (character == 'e' && couldBe[9] == 3)
         {
            couldBe[9] = 0;
            digit = 9;
         }
         else
         {
            couldBe[9] = 0;
         }

         printf("c(%c) ", line[i]);
         if ('0' <= line[i] && line[i] <= '9')
         {
            // print character
            digit = line[i] - '0';
            printf("digit(%d) ", digit);
         }

         if (digit != -1)
         {
            if (first == -1)
            {
               first = digit;
               last = digit;
            }
            else
            {
               last = digit;
            }
         }
      }

      // print line, first and last
      printf("%s %d %d\n", line, first, last);
      int value = 10 * first + last;
      total += value;
   }

   fclose(file);
   *result = total;
   return 0;
}

int main()
{
   int result;
   if (part2(&result) == 0)
   {
      printf("Number of lines: %d\n", result);
   }
   else
   {
      printf("Error reading file\n");
   }
   return 0;
}
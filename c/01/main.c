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

int main()
{
   int result;
   if (part1(&result) == 0)
   {
      printf("Number of lines: %d\n", result);
   }
   else
   {
      printf("Error reading file\n");
   }
   return 0;
}
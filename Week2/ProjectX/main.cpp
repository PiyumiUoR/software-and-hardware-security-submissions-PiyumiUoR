#include "src/calculator.h"

int main() {
    // Function calls to calculate
    // Write your all possible input scenarios here
    // Notice that calculator code written here works with integers

    calculator(2, '+' , 8);
    calculator(890, '-' , 146);
    calculator(3549713, '/' , 7);
    calculator(598776, '*' , 2);
    calculator(5, '/' , 0);
    calculator(65 '%' , 5);
    calculator(1000000, '*' , 1000000);
    calculator(INT_MIN, '+' , 2);
    calculator(INT_MAX, '-' , 1);
    calculator(1, '/' , 5);
    calculator(-2, '*' , 5);
    calculator(1000000000, '*' , 8);

    
    return 0;
}

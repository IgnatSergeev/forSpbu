#include <stdio.h>
#include "customStack.h"
#include <malloc.h>

int main() {
    Stack* stack = createStack();
    push(stack, 'a');
    printf("%c", top(stack));

    free(stack);
    return 0;
}
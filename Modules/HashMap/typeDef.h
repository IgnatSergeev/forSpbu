#pragma once

#include <stdlib.h>

typedef struct Type {
    char *string;
    int stringSize;
} Type;

int hashFunction(Type key);

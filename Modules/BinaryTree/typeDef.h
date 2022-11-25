#pragma once

#include <stdbool.h>

enum TypesOfExpression{
    operation,
    number
};

typedef struct Type {
    enum TypesOfExpression type;
    char operation;
    int number;
    bool isSubtreeFull;
} Type;
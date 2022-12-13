#pragma once

#include "typeDef.h"

#define HASH_FUNCTION_RANGE 256

//hashFunction should return number >= 0 and < HASH_FUNCTION_RANGE
int hashFunction(KeyType key) {
    return 0;
}
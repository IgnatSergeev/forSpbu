#pragma once

#include <stdbool.h>
#include "typeDef.h"
#include "hashFunction.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, Type value, int (*compare)(Type, Type), Type (*whatIfEqualInAdding)(Type, Type));

void clearHashMap(HashMap *hashMap);
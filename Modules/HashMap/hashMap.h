#pragma once

#include <stdbool.h>
#include "typeDef.h"
#include "hashFunction.h"

typedef struct HashMap HashMap;

HashMap *createHashMap();

int addValue(HashMap *hashMap, KeyType key, ValueType value);

void clearHashMap(HashMap *hashMap);
#pragma once

typedef struct EdgeProperties {
    int endNodeIndex;
    int length;
} EdgeProperties;

typedef struct NodeData {
    int index;
    int countryIndex;//-1 - none
    int *distancesToTheCapitals;
} NodeData;

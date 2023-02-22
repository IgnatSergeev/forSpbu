#pragma once

#include "typeDef.h"
#include <stdbool.h>

typedef struct Graph Graph;

typedef struct NodesDataList NodesDataList;

Graph *createGraph(int numberOfNodes, NodeData nodesData[]);

void clearGraph(Graph *graph);

void changeNodeData(Graph *graph, int index, NodeData nodeData);

void addEdge(Graph *graph, int indexOfStartNode, int indexOfEndNode, int length);

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex, bool *isVisited);

void breadthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int nodeIndex);

int *minimalDistancesFromNodeIndexToEachNode(Graph *graph, int nodeIndex);

int **minimalDistancesFromEachToEachNode(Graph *graph);

int addNodeToTheCountry(Graph *graph, NodesDataList *list, int countryIndex);
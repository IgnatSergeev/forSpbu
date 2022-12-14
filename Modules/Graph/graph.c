#include "graph.h"
#include "malloc.h"
#include "typeDef.h"

typedef struct Node {
    int nodeIndex;
    Type value;
    struct Node *next;
} Node;

typedef struct Graph {
    int graphSize;
    struct Node **adjacencyList;
} Graph;

Graph *createGraphWithAdjacencyMatrix(int numberOfNodes) {
    Graph *graph = calloc(1, sizeof(Graph));
    if (graph == NULL) {
        return NULL;
    }
    graph->adjacencyList = calloc(numberOfNodes, sizeof(int *));
    if (graph->adjacencyList == NULL) {
        free(graph);
        return NULL;
    }

    return graph;
}

int addEdge(Graph *graph, int nodeFrom, int nodeIn) {
    Node *arrayOfConnectedNodes = graph->adjacencyList[nodeFrom];
}
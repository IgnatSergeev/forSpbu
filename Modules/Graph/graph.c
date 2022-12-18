#include "graph.h"
#include "malloc.h"
#include "typeDef.h"
#include "stdbool.h"
#include "edgesList.c"

typedef struct Graph {
    int graphSize;
    List **adjacencyList;
} Graph;

Graph *createGraph(int numberOfNodes, NodeData nodesData[]) {
    Graph *graph = calloc(1, sizeof(Graph));
    if (graph == NULL) {
        return NULL;
    }
    graph->graphSize = numberOfNodes;
    graph->adjacencyList = calloc(numberOfNodes, sizeof(List *));
    if (graph->adjacencyList == NULL) {
        free(graph);
        return NULL;
    }
    for (int i = 0; i < numberOfNodes; i++) {
        graph->adjacencyList[i] = create();
        graph->adjacencyList[i]->nodeData = nodesData[i];
        if (graph->adjacencyList[i] == NULL) {
            for (int j = 0; j < i; j++) {
                clear(graph->adjacencyList[j]);
            }
            free(graph->adjacencyList);
            free(graph);
            return NULL;
        }
    }

    return graph;
}

void clearGraph(Graph *graph) {
    for (int j = 0; j < graph->graphSize; j++) {
        clear(graph->adjacencyList[j]);
    }
    free(graph->adjacencyList);
    free(graph);
}

int addEdge(Graph *graph, int indexOfStartNode, EdgeProperties edgeProperties) {
    List *listOfConnectedNodes = graph->adjacencyList[indexOfStartNode];
    return insertNodeToEnd(listOfConnectedNodes, edgeProperties);
}

void depthFirstSearch(Graph *graph, NodeData (*whatToDoWithTheValue)(NodeData), int currentNodeIndex) {
    List *listOfConnectedNodes = graph->adjacencyList[currentNodeIndex];
    Edge *iteratorNode = listOfConnectedNodes->head;
    while (iteratorNode != NULL) {
        depthFirstSearch(graph, whatToDoWithTheValue, iteratorNode->value.endNodeIndex);
        iteratorNode = iteratorNode->next;
    }
    listOfConnectedNodes->nodeData = (*whatToDoWithTheValue)(listOfConnectedNodes->nodeData);
}
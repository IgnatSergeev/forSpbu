@echo off

start dot -Tpng ../30.11/graphViz.dot -o graphViz.png

timeout 1

start ../30.11/graphViz.png
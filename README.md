# OnTheBeachJob
Assignment for OnTheJob organization. 
# Environment
IDE: Visual studio 2019
<BR>
Framework: .Net Core 2.1
<BR>
Unit Test Tool: xUnit
<BR>
  
## The Challenge

Imagine we have a list of jobs, each represented by a character. Because certain jobs must be done before others, a job may have a
dependency on another job. For example, a may depend on b, meaning the final sequence of jobs should place b before a. If a has no
dependency, the position of a in the final sequence does not matter.
<BR>
  <H4> Scenarios: </H4>
    
1) Given you’re passed an empty string (no jobs), the result should be an empty sequence.
Given the following job structure:
<BR> a =>
<BR>
   
The result should be a sequence consisting of a single job a.
<BR>
<BR>
  
2) Given the following job structure:
 
 ```a =>
 b =>
 c => 
 ```
The result should be a sequence containing all three jobs abc in no significant order.
<BR><BR>
  
3) Given the following job structure:
 ```a =>
 b => c
 c => 
 ```
The result should be a sequence that positions c before b, containing all three jobs abc.
<BR>
<BR>

4) Given the following job structure:
 ```a =>
 b => c
 c => f
 d => a
 e => b
 f => 
 ```
The result should be a sequence that positions f before c, c before b, b before e and a before d containing all six jobs abcdef.
<BR>
<BR>
    
5) Given the following job structure:
 ```a =>
 b =>
 c => c
 ```
The result should be an error stating that jobs can’t depend on themselves.
<BR>
<BR>

6) Given the following job structure:
```a =>
b => c
c => f
d => a
e =>
f => b
```
The result should be an error stating that jobs can’t have circular dependencies.

## License
[MIT](https://choosealicense.com/licenses/mit/)

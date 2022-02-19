# Implementation Spec
## About the data structures
This program will primarily utilizes the 
counters data structure, to store elements
accessed from the index (a hashtable of 
countersets).
See index.h, hashtable.h and counters.h 
for ind-depth details on each of these 
data structures.

**Index**: *"an inverted-index data structure mapping from words to (documentID, count) pairs* - CS50 site, Lab 5/Lecture 19
The index data structure is a 
C-representation of an inverted-index. 
It is a ***hashtable of countersets***, 
which maps from a word to a list of 
(docID, count) pairs. More precisely, 
it is a list of sets, each of which 
contains a single key - a word, and a 
single item - a counterset (which itself
contains the aforementioned docID, count 
pairs, which represent the \# of 
occurances of a given word in each 
document). The hashtable and the sets
it contains share the same keywords
(with the hashtable of course storing
those words as a hashed integer). This
makes traversal of the structure very 
straightforward, as there is effectively
only one level of key traversal between 
both the hashtable and the list of sets.

**Hashtable**: *"a set of (key, item) pairs."* - CS50 site, Lab 3
Similar to a set, but more efficient for 
larger collections.
Stores key/item pairs (char *key, void
*item), has no order, 
retrieval by key, does not allow duplicate
key/item pairs.
Note: our hashtable (and all hashtables)
utilizes a hash function to allow for
properly map the keys. The function we use
is the *Jenkins Hash function* (found in
jhash.c), which is specifically made for 
hashing key strings (convenient for our  
purposes: we will be hashing urls 
represented as strings)

**Counters**: *"a set of counters, each distinguished by an integer ***key***. "* - CS50 site, Lab 3
It’s a set - each ***key*** can only occur
once 
in the set - and it tracks a ***counter***
for 
each ***key***. It starts empty. Each time 
counters_add is called on a given ***key***,
the corresponding ***counter*** is incremented. 
The current ***counter*** value can be 
retrieved by asking for the relevant 
***key***.

*NOTE:* Index is a hashtable of counters, so 
iterative helper methods must be created in
order to implement most forms of functionality.
HOWEVER, because hashtables are a list of 
***sets***, they implicitly call set_iterate
when the user calls hashtable_iterate. In
other words, there only needs to be one level
of nesting iterators when creating helper
methods (hashtable_iterate and counters_iterate)

# About the modules
**Webpage**: *"utility functions for*
*downloading, saving, and loading web*
*pages"* - webpage.h
Members include: 
int depth - depth of crawl
char *url - url stored as cstring
char *html - html stored as cstring
The webpage module provides most of the 
functionality for traversing and collecting
data from websites. It also handles most
of the resource management in terms of
allocating and freeing memory. However,
we must be sure to allocate memory to store
*copies* of the user inputs (seedURL, 
pageDirectory, and maxDepth). Using the 
originals will lead to risk of memory leaks.

**pagedir**: *"provides some helper*
*functions for traversing and collecting*
*data from webpages"*
 * `pagescanner` - scans webpage for urls
 * `pagefetcher` - fetches html content from webpage
 * `pagesaver` - stores html content in uniq file
 * `isCrawlerDirectory` - checks if directory was accessed by crawler
 * `isWritable` - checks if file is writable
 * `pageload` - loads contents of file into new webpage struct

**index**: *"contains all the logic for*
*saving and loading index files"*
 * `getcs` - getter funtion for the hashtable
 * `index_new` - creates new index
 * `index_delete` - deletes the index
 * `index_build` - builds the index from crawler's
                   director of files
 * `index_save` - saves the index to a file

**word**: *"contains functions for cleaning up strings"*
 * `NormalizeWord` - converts a word to lower-case
 * `sep_words` - separates string into array of space-delimited strings


## Security and privacy properties
Global variables are not advised 
(and not necessary).

## Error handling and recovery
There should be no errors, as long as
memory is handled appropriately.
Remember to only use copies of pointers
(of counters_t pointers, in particular) when 
performing any sort of operation.


## Resource Management
The querier methods are not as advanced
- in terms of resource use -, as those for
indexer and crawler. (much of the memory
allocation required for the TSE program 
was for the collection and storage of 
the webpage data (in the crawler module) 
and the conversion/storage of date (in 
the indexer module). As long as those
modules were implemented properly, 
the querier requires little in the ways
of argument validation 


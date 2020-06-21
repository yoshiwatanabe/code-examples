// All things brought in by node's module wrapper function as function arguments

console.log(module);
console.log(require);
console.log(exports)
console.log(__filename);
console.log(__dirname);

let globalVariable = 123; // actually scoped to this module


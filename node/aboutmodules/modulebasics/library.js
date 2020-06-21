const util = require("./utility");
const WAStateTax = .098;

exports.GetTaxedPrice = (price, state) => {
    switch (state) {
        case "WA":
            return util.add(price, util.multiply(price, WAStateTax));
        default:
            throw "not yet implemented";
    }
}
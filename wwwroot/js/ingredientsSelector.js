let counter = 0;
let defaultIngredient;

$(document).ready(function () {
    defaultIngredient = $("#defaultIngredient").clone();

    $("#btnAddIngredient").click(function () {
        addIngredient();
    });
});

function addIngredient() {
    if (counter >= 99) {
        return;
    }
    counter++;

    var newIngredient = defaultIngredient
        .clone();
    newIngredient.find('select:first')
        .attr("name", "IngredientInputs[" + counter + "].IngredientId");
    newIngredient.find('input:first')
        .attr("name", "IngredientInputs[" + counter + "].Quantity");
       
    $('#ingredientContainer').append(newIngredient); 
}

function removeIngredient(button) {
    if (counter <= 0) {
        return;
    }
    counter--;

    button.parentNode.parentNode.remove();
}
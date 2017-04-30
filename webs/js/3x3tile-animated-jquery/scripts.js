$(document).ready(function() {

  // Select the middle tile on document load.
  $(".tile:nth-child(5)").addClass("selected")

  // Any tile with a selection device on it should indicate that it is selectable.
  $(".tile").hover(
    function() {
      $(this).toggleClass("aimed");
      if (!$(this).hasClass("selected")) {
        $(this).addClass("selectable")
      }
    },
    function() {
      $(this).toggleClass("aimed");
      if (!$(this).hasClass("selected")) {
        $(this).removeClass("selectable")
      }
    })

  // When a tile is clicked, depending on whether it is selected or not
  // changes the state to selected or completely unselected.
  $(".tile").click(
    function() {
      if (!$(this).hasClass("selected")) {
        $(".tile").removeClass("selected selectable")
        $(this).addClass("selected")
      } else if ($(this).hasClass("selected")) {
        $(this).removeClass("selected selectable")
      }
    }
  )
})

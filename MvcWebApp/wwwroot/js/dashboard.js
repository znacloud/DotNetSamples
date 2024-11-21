$(document).ready(function () {
  const icon = $("#MenuIcon");
  icon.click(function () {
    const menu = $("#MenuPanel");
    menu.toggle();
    console.log(icon.attr("src"));
    if(icon.attr("src").includes("outdent")){
      icon.attr("src", "/imgs/indent.svg");
    }else{
      icon.attr("src", "/imgs/outdent.svg");
    }
  });

  const profile = $("#ProfilePanel");
  $("#ProfileIcon").click((e) => {
    e.stopImmediatePropagation();
    profile.toggleClass("show");
  });

  profile.click((e) => e.stopImmediatePropagation());
  $("#dashboard").click((e) => {
    e.stopImmediatePropagation();
    profile.removeClass("show");
  });
});

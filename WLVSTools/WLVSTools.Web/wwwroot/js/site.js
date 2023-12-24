var WLVSTools_ActiveSideMenuParent = "WLVSTools_ActiveSideMenuParent";
var WLVSTools_ActiveSideMenu = "WLVSTools_ActiveSideMenu";
$(function () {
    $("a.nav-link").on("click", function () {
        if ($(this).parents().is(".sidebar")) {
            $(".sidebar").children("a").removeClass("active");

            let parentMenuId = $(this).parents("ul[data-parent-menu-id]").data("parent-menu-id");
            let activeSideMenuId = $(this).attr("id");

            localStorage.setItem(WLVSTools_ActiveSideMenuParent, parentMenuId);
            localStorage.setItem("WLVSTools_ActiveSideMenu", activeSideMenuId);

            setActiveSideMenu();
        }
    });

    setActiveSideMenu();
});

function setActiveSideMenu() {
    let activeParentMenuId = localStorage.getItem(WLVSTools_ActiveSideMenuParent);
    let activeParentMenu = $("#" + activeParentMenuId);
    activeParentMenu.addClass("active");
    activeParentMenu.parent("li").addClass("menu-open");

    let activeSideMenuId = localStorage.getItem(WLVSTools_ActiveSideMenu);
    $("#" + activeSideMenuId).addClass("active");
}
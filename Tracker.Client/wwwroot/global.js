function scrollEventListener() {
    const scrollProgressElement = document.querySelector("#scroll-bar");
    const articleSection = document.querySelector("article.content");

    if (articleSection && scrollProgressElement) {
        articleSection.addEventListener(
            "scroll",
            () => {
                scrollProgressElement.style.height = Math.round(
                    (articleSection.scrollTop / (articleSection.scrollHeight - articleSection.clientHeight)) * 100
                ) + "%";
            }
        );
    }
}
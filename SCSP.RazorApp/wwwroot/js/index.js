// Mobile menu toggle
function initializeHeader() {
    const mobileMenuBtn = document.getElementById("mobile-menu-btn");
    const navMenu = document.getElementById("nav-menu");

    if (mobileMenuBtn && navMenu) {
        mobileMenuBtn.addEventListener("click", () => {
            navMenu.classList.toggle("active");

            // Animate hamburger menu
            const spans = mobileMenuBtn.querySelectorAll("span");
            if (navMenu.classList.contains("active")) {
                spans[0].style.transform = "rotate(45deg) translate(5px, 5px)";
                spans[1].style.opacity = "0";
                spans[2].style.transform = "rotate(-45deg) translate(7px, -6px)";
            } else {
                spans[0].style.transform = "none";
                spans[1].style.opacity = "1";
                spans[2].style.transform = "none";
            }
        });

        // Close mobile menu when clicking outside
        document.addEventListener("click", (event) => {
            if (!event.target.closest(".nav-container")) {
                navMenu.classList.remove("active");
                const spans = mobileMenuBtn.querySelectorAll("span");
                spans[0].style.transform = "none";
                spans[1].style.opacity = "1";
                spans[2].style.transform = "none";
            }
        });
    }
}

// Stats counter animation
function initializeStatsCounter() {
    const statNumbers = document.querySelectorAll(".stat-number");

    const animateCounter = (element) => {
        const target = Number.parseInt(element.getAttribute("data-target"));
        const duration = 2000; // 2 seconds
        const increment = target / (duration / 16); // 60fps
        let current = 0;

        const timer = setInterval(() => {
            current += increment;
            if (current >= target) {
                current = target;
                clearInterval(timer);
            }
            element.textContent = Math.floor(current).toLocaleString("vi-VN");
        }, 16);
    };

    // Intersection Observer for triggering animation when in view
    const observer = new IntersectionObserver(
        (entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    animateCounter(entry.target);
                    observer.unobserve(entry.target);
                }
            });
        },
        {
            threshold: 0.5,
        }
    );

    statNumbers.forEach((stat) => {
        observer.observe(stat);
    });
}

// Smooth scrolling for anchor links
function initializeSmoothScroll() {
    document.addEventListener("click", (e) => {
        if (e.target.matches('a[href^="#"]')) {
            e.preventDefault();
            const target = document.querySelector(e.target.getAttribute("href"));
            if (target) {
                target.scrollIntoView({
                    behavior: "smooth",
                    block: "start",
                });
            }
        }
    });
}

// Header scroll effect
function initializeScrollEffect() {
    window.addEventListener("scroll", () => {
        const header = document.querySelector(".header");
        if (header) {
            if (window.scrollY > 100) {
                header.style.background = "rgba(255, 255, 255, 0.95)";
                header.style.backdropFilter = "blur(10px)";
            } else {
                header.style.background = "white";
                header.style.backdropFilter = "none";
            }
        }
    });
}

// Initialize all functions when DOM is loaded
document.addEventListener("DOMContentLoaded", () => {
    initializeHeader();
    initializeStatsCounter();
    initializeSmoothScroll();
    initializeScrollEffect();

    // Add loading animation
    document.body.classList.add("loaded");

    // Handle "Learn More" button click
    document.addEventListener("click", (e) => {
        if (e.target.matches(".btn-secondary")) {
            e.preventDefault();
            // Scroll to features section
            const featuresSection = document.querySelector(".features-section");
            if (featuresSection) {
                featuresSection.scrollIntoView({
                    behavior: "smooth",
                    block: "start",
                });
            }
        }
    });
});
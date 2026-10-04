// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Global Rich Text Editor Logic
document.addEventListener('DOMContentLoaded', () => {
    const toggleRichText = document.getElementById('toggleRichText');
    const richTextToolbar = document.getElementById('richTextToolbar');
    const textarea = document.getElementById('Description'); 

    if (toggleRichText && richTextToolbar && textarea) {
        // 1. Create content editable div
        const editorDiv = document.createElement('div');
        editorDiv.className = textarea.className;
        editorDiv.style.minHeight = '150px';
        editorDiv.style.overflowY = 'auto';
        editorDiv.contentEditable = 'true';
        editorDiv.innerHTML = textarea.value;
        
        // Insert after textarea
        textarea.parentNode.insertBefore(editorDiv, textarea.nextSibling);

        // 2. Map buttons in the toolbar
        const commandMap = {
            'bi-type-bold': 'bold',
            'bi-type-italic': 'italic',
            'bi-type-underline': 'underline',
            'bi-list-ul': 'insertUnorderedList',
            'bi-list-ol': 'insertOrderedList',
            'bi-link': 'createLink',
            'bi-link-45deg': 'createLink',
            'bi-image': 'insertImage'
        };

        const buttons = richTextToolbar.querySelectorAll('button');
        buttons.forEach(btn => {
            const icon = btn.querySelector('i');
            if (icon) {
                const iconClass = Array.from(icon.classList).find(c => c.startsWith('bi-'));
                if (iconClass && commandMap[iconClass]) {
                    btn.setAttribute('data-command', commandMap[iconClass]);
                    btn.addEventListener('click', (e) => {
                        e.preventDefault();
                        const cmd = btn.getAttribute('data-command');
                        if (cmd === 'createLink') {
                            const url = prompt('Enter link URL:');
                            if (url) document.execCommand(cmd, false, url);
                        } else if (cmd === 'insertImage') {
                            const url = prompt('Enter image URL:');
                            if (url) document.execCommand(cmd, false, url);
                        } else {
                            document.execCommand(cmd, false, null);
                        }
                        editorDiv.focus();
                    });
                }
            }
        });

        // 3. Sync content on input and form submit
        editorDiv.addEventListener('input', () => {
            textarea.value = editorDiv.innerHTML;
        });

        textarea.addEventListener('input', () => {
            editorDiv.innerHTML = textarea.value;
        });
        
        const form = textarea.closest('form');
        if (form) {
            form.addEventListener('submit', () => {
                if (toggleRichText.checked) {
                    textarea.value = editorDiv.innerHTML;
                }
            });
        }

        // 4. Toggle Logic
        toggleRichText.addEventListener('change', (e) => {
            if (e.target.checked) {
                richTextToolbar.classList.remove('d-none');
                richTextToolbar.classList.add('d-flex');
                editorDiv.style.display = 'block';
                textarea.style.display = 'none';
                editorDiv.innerHTML = textarea.value; // Sync up
            } else {
                richTextToolbar.classList.remove('d-flex');
                richTextToolbar.classList.add('d-none');
                editorDiv.style.display = 'none';
                textarea.style.display = 'block';
                textarea.value = editorDiv.innerHTML; // Sync up
            }
        });

        // Initialize state
        toggleRichText.dispatchEvent(new Event('change'));
    }
});

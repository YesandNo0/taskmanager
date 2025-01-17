﻿const deleteButtons = document.querySelectorAll('.action-btn-delete');
const modal = document.querySelector('.modal');
const value = document.querySelector('.modal-value');
const confirmDelete = document.querySelector('.modal-delete');
const cancelDelete = document.querySelector('.modal-cancel');

const API_DELETE = 'http://localhost:5157/tasks/delete';

const deleteTask = async taskId => {
    const request = `${API_DELETE}/${taskId}`;
    const options = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        }
    };

    try {
        const response = await fetch(request, options);
        return (response.status === 200);
    } catch(error) {
        return false;
    }
}

deleteButtons.forEach(btn => {
    btn.addEventListener('click', () => {
        let oldValue = value.classList[1];
        value.classList.replace(`${oldValue}`, btn.classList[2]);
        modal.classList.replace('hidde', 'show');
    });
});

cancelDelete.addEventListener('click', () => {
    modal.classList.replace('show', 'hidde');
});

confirmDelete.addEventListener('click', async () => {
    let taskId = (Number)(value.classList[1]);
    let result = await deleteTask(taskId);

    if (result) {
        let deleteTasks = document.querySelectorAll('.task');
        for (const delTask of deleteTasks) {
            if ((Number)(delTask.classList[1]) === taskId) {
                delTask.remove();
                break;
            }
        }
    } else {
        console.error('ocurred an error while you marked as deleted this task');
    }

    modal.classList.replace('show', 'hidde');
});
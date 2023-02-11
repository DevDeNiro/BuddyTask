<template>

  <div class="w-10/12 self-center">
    <p class="block text-sm py-3 font-medium text-gray-700">Create new task</p>
        <form action="#" method="POST">
          <div class="overflow-hidden shadow sm:rounded-md">
            <div class="bg-white px-4 py-5 sm:p-6">
              <div class="grid grid-cols-6 gap-6">
                <div class="col-span-6 sm:col-span-3">
                  <label for="first-name" class="block text-sm font-medium text-gray-700">First name</label>
                  <input type="text" name="first-name" id="first-name" autocomplete="given-name" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                </div>

                <div class="col-span-6 sm:col-span-3">
                  <label for="last-name" class="block text-sm font-medium text-gray-700">Due date</label>
                  <input type="date" class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500 sm:text-sm" />
                </div>
              </div>
              <button class="bg-white hover:bg-gray-100 text-gray-800 font-semibold py-1 px-2 mt-4 border border-gray-400 rounded shadow">
                Create
              </button>
            </div>
          </div>
        </form>
  </div>
</template>


<script>

import Datepicker from 'vuejs3-datepicker';

export default {

  name: "HomeView",
  components: {
    Datepicker
  }
  };

</script>
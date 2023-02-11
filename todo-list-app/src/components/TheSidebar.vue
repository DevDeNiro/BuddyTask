<style>

</style>

<template>
    <div class="w-2/12 min-h-screen bg-gray-100">
        <div class="min-h-screen border-l-indigo-500 shadow-xl flex flex-col bg-white  overflow-hidden">
            <div class="flex items-center justify-center h-20 shadow-md">
                <h1 class="text-3xl uppercase text-indigo-500">Main</h1>
            </div>
                <ul class="flex flex-col py-4">
                    <li class="border-l-green-500 ">
                        <a href="#" class="flex flex-row items-center h-12 transform hover:translate-x-2 transition-transform ease-in duration-200 text-gray-500 hover:text-gray-800">
                        <span class="inline-flex items-center justify-center h-12 w-12 text-lg text-gray-400"><i class="bx bx-home"></i></span>
                        <span class="focus:border-green-400  text-sm font-medium">Dashboard</span>
                        </a>
                    </li>
                </ul>
        </div>
    </div>
</template>

<script>
export default {
    name: "HomeView",
};

</script>